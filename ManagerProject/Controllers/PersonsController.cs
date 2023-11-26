using ManagerApp.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;
using Services;
using Services.DTO;
using System.Text;
using System.Text.Json;

namespace ManagerApp.Controllers;

[Route("[controller]")]
[ModelValidationActionFilter]
[Authorize]
public class PersonsController : Controller
{
    private ICountriesService _countriesService;
    private IPersonService _personService;
    private ILogger<PersonsController> _logger;
    private IUsersActivityService _userService;
    public PersonsController(ICountriesService countriesService, IPersonService personService, ILogger<PersonsController> logger, IUsersActivityService userService)
    {
        _countriesService = countriesService;
        _personService = personService;
        _logger = logger;
        _userService = userService;
    }

    [AllowAnonymous]
    [Route("/")]
    [Route("[action]")]
    public IActionResult Index()
    {
        return View();
    }

    [Route("show")]
    public async Task<IActionResult> ShowAll()
    {
        List<PersonResponce> all = await _personService.GetAllPersons();
        return View(all);
    }

    [SetSearchOptionActionFilter]
    [Route("[action]")]
    public async Task<IActionResult> Search(string searchFilter, string searchText)
    {
        List<PersonResponce> people = await _personService.GetFilteredByAny(searchFilter, searchText);

        var peopleBytes = JsonSerializer.SerializeToUtf8Bytes(people);
        HttpContext.Session.Set("FilteredPeople", peopleBytes);

        return View(people);
    }

    [Route("[action]/{sortFilter}/{sortOrder}")]
    public async Task<IActionResult> Sort(string sortFilter, SortOrder sortOrder)
    {
        List<PersonResponce> responces = await _personService.GetAllPersons();
        ViewBag.SortFilter = sortFilter;
        ViewBag.SortOrder = sortOrder.ToString();
        List<PersonResponce> people = _personService.GetSorted(responces, sortFilter, sortOrder);
        return View(people);
    }

    [Route("filtered-sort/{sortFilter}/{sortOrder}")]
    public async Task<IActionResult> FilteredSort(string sortFilter, SortOrder sortOrder)
    {
        ViewBag.SortFilter = sortFilter;
        ViewBag.SortOrder = sortOrder.ToString();
        var context = HttpContext.Session.Get("FilteredPeople");
        if (context == null)
            return RedirectToAction(nameof(ShowAll));

        var stream1 = new MemoryStream(context);

        List<PersonResponce> people = await JsonSerializer.DeserializeAsync<List<PersonResponce>>(stream1);

        List<PersonResponce> sortedPeople = _personService.GetSorted(people, sortFilter, sortOrder);
        return View(sortedPeople);
    }

    [Route("create-person")]
    [HttpGet]
    public async Task<IActionResult> CreatePerson()
    {
        List<CountryResponce> countries = await _countriesService.GetAllCountries();
        ViewBag.Countries = countries.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
        return View();
    }

    [Route("create-person")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreatePerson(PersonAddRequest person)
    {
        //check if such email exists
        bool existEmail = false;
        if (person.Email != null)
            existEmail = await _personService.CheckEmailExists(person.Email);

        if (existEmail)
        {
            ModelState.AddModelError(nameof(PersonAddRequest.Email), "This email address already exists");
            List<CountryResponce> countries = await _countriesService.GetAllCountries();
            ViewBag.Countries = countries.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            return View();
        }

        await _personService.AddPerson(person);
        await _userService.UpdateTimesCreated(User.Identity.Name);

        return RedirectToAction(nameof(ShowAll));
    }

    [Route("edit/{id}")]
    [HttpGet]
    public async Task<IActionResult> EditPerson(Guid id)
    {
        PersonResponce? person = await _personService.GetPerson(id);
        if (person == null)
            return RedirectToAction(nameof(ShowAll));

        List<CountryResponce> countries = await _countriesService.GetAllCountries();
        ViewBag.Countries = countries.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });

        PersonUpdateRequest personUpdateRequest = PersonUpdateRequest.ToPersonUpdate(person);
        return View(personUpdateRequest);
    }

    [Route("edit/{id}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditPerson(PersonUpdateRequest person)
    {
        //check if the email has changed
        bool existEmail = false;
        if (person.Email != null)
        {
            PersonResponce? personResponce = await _personService.GetPerson(person.Id);
            if (personResponce?.Email != person.Email)
                existEmail = await _personService.CheckEmailExists(person.Email);
        }

        if (existEmail)
        {
            ModelState.AddModelError(nameof(PersonUpdateRequest.Email), "This email address already exists");
            List<CountryResponce> countries = await _countriesService.GetAllCountries();
            ViewBag.Countries = countries.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() });
            return View(person);
        }

        await _personService.UpdatePerson(person);
        await _userService.UpdateTimesEdited(User.Identity.Name);
        return RedirectToAction(nameof(ShowAll));
    }

    [Route("delete/{id}")]
    [HttpGet]
    public async Task<IActionResult> DeletePerson(Guid id)
    {
        PersonResponce? personResponce = await _personService.GetPerson(id);
        if (personResponce == null)
            return RedirectToAction(nameof(ShowAll));

        return View(personResponce);
    }

    [Route("delete/{id}")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeletePerson(PersonResponce person)
    {
        await _personService.DeletePerson(person.ID);
        await _userService.UpdateTimesDeleted(User.Identity.Name);
        return RedirectToAction(nameof(ShowAll));
    }

    [Route("personstopdf")]
    public async Task<IActionResult> PersonsToPdf()
    {
        List<PersonResponce> people = await _personService.GetAllPersons();
        return new ViewAsPdf(people)
        {
            FileName = "AllPersons.pdf",
            PageSize = Size.A4,
            PageOrientation = Orientation.Portrait,
        };
    }

    [HttpGet("keklol")]
    public async Task<IActionResult> Kek()
    {
        HttpClient client = new HttpClient();
        var city = new { Name = "Hello" };
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7222/api/citiesapi") { Method = HttpMethod.Post };
        request.Content = new StringContent(JsonSerializer.Serialize(city), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.SendAsync(request);
        return Ok(response.IsSuccessStatusCode);
    }
}
