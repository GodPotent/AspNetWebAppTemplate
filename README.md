# ASP.NET WebApp Template

## Tasks

### Week One

- [x] Create ASP.NET project
- [x] Using pgAdmin 4, create database `aspnet-blog`
- [ ] Debug app in hot-reload mode
- [ ] Install Npgsql.EntityFrameworkCore.PostgreSQL
- [ ] Connect to PostGreSQL database
    - [ ] Add the connection string of the database created to `appsettings.Development.json`
    - [ ] [For our ER Diagram, initialize the Database Tables](https://jasonwatmore.com/net-7-postgres-connect-to-postgresql-database-with-dapper-in-c-and-aspnet-core)
- [ ] Create a class for the User table (`Data/Model/User.cs`)

### Week Two

- [ ] Follow [ASP.NET Core Hyphen Separated (Kebab) Routes](https://blog.elijahlopez.ca/posts/aspnet-hyphen-separated-routes/)
	- If anything is unclear, provide feedback: *INSERT FEED BACK HERE*
- [ ] Create signup form user interface via Razor pages (`cshtml` files)
    - Ask an LLM to create an HTML form for user signups with `action="/signup"`
- [ ] Create a file `AuthController.cs` in a new `PROJECT/Controller` directory
- [ ] Create a signup route in ASP.NET to serve the file above (ASP.NET can handle cshtml files and serve them)
	- A route is a handler (function) that deals with requests pertaining to a URL (e.g. `http://localhost:2049/signup`)
- [ ] Create a signup route in ASP.NET to handle the `POST` requests created by the form above
    - Note that it might not be possible to handle both POST and GET requests in two different functions. In that case, just change the action of the form to `signup/complete`
- [ ] Create signup route, encrypt passwords using PasswordHasher
- [ ] Create `/login` route, verify password, need signed cookie based authentication
- [ ] Create a `/profile`
- [ ] Create class for the Blog table

### Future Week 1

1. Do a database migration so that authorized editors column exists
2. Impement logic related to authorized editors column

### Future Week 2

This week focuses on improving the login page. Suppose an anonymous user comes accross a blog post and clicks on the edit button.
The user is not authenticated yet, so we need to make them login. If the user is logged in, check if they are allowed to edit it.
If they are not allowed to edit it, show the same blog post, but with a notification-like message (a JavaScript `alert()` sufffices, although [flashing messages](#flashing-messages-example) is less intrusive)

- [ ] In the `login` route, provide functioanlity to redirect after successful login
	- Sometimes when we access web pages, we are asked to login. A good user experience redirects the user to the resource they were requesting initially
	- There are two ways to do this. One way is to use a query parameter for the path to redirect to
	- The other way that I have not tried, is to set a cookie before redirecting to login, and then post login, pop this cookie and redirect the user again
        - I personally prefer this way as then the user can bookmark a page and not the login

## Resources

- Database
	- PostgreSQL
		- [Npgsql](https://www.npgsql.org/doc/index.html)
		- [How to get the Connection String](https://hasura.io/learn/database/postgresql/installation/2-postgresql-connection-string/)
	- [Blog E-R Diagram](https://app.diagrams.net/#W760C84686D150D74%2F760C84686D150D74!s5250331e36ef482ab85114112bb1e0f2#%7B%22pageId%22%3A%22R2lEEEUBdFMjLlhIrx00%22%7D)
		- Shared via OneDrive
- Auth
	- [PasswordHasher](https://andrewlock.net/exploring-the-asp-net-core-identity-passwordhasher/)

### Flashing Messages Example

When I built an [ecommerce site](https://lenerva.com/store) from scratch,
I copied [this notifcation on my website when the user clicks "Monero Address" to copy](https://elijahlopez.ca/social/) such that 
these snackbars can be shown when a page is loaded rather than using JavaScript to invoke the snackbar. This implementation uses Flask's `flash` API.
I'm sure a similar API exists for ASP.NET Razor Pages.

```jinja2
{% with messages = get_flashed_messages(True) %}
    {% if messages %}
    <div class="snackbar show">
        {% for category, message in messages %}
            {% if category == 'message' %}
                <p class="flashed-msg green">{{ message }}</p>
            {% elif category == 'error' %}
                <p class="flashed-msg red">{{ message }}</p>
            {% elif category == 'warn' %}
                <p class="flashed-msg yellow">{{ message }}</p>
            {% endif %}
        {% endfor %}
    </div>
    {% endif %}
{% endwith %}
```