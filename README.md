# ASP.NET WebApp Template

Instructions created by [Elijah Lopez @elibroftw](https://github.com/elibroftw/).

The purpose of this repo is twofold; Although the primary reason is to create a starter template when writing a web application in ASP.NET + Razor Page (with support for others later), the journey to get there will make someone a proficient backend developer. These tasks exist to push a developer to use their tools the right way rather than just writing the code. There is plently of tacit knowledge that cannot be transferred simply by reading code. The tasks given below are to be as simple to interpret as possible and pointers are given when the task requires knowledge that cannot be attained through intuition. Examples include: selecting a database, avoiding SQL injection attacks, dealing with CSRF, authentication (future)

## Tasks

### Week One

- [x] Create ASP.NET project
- [x] Using pgAdmin 4, create database `aspnet-blog`
- [ ] Debug app in hot-reload mode
- [ ] Install Npgsql.EntityFrameworkCore.PostgreSQL
- [x] Connect to PostGreSQL database
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

### Future Task One

Integration tesitng of the backend.

- [ ] Add a health check endpoint that can be hit by users signed into the username `admin`. The health check should try to run every database query in dry mode such that if a developer messed up, the health check should return a 500 internal server error. Therefore, ensure that all the queries we use are in prepared statements are in a single file. The health check is expected to return a 204.
- [ ] Delete username `integration-test`
- [ ] Test sign up with username `integration-test` and password `!";#$%&'()*+,-./:;<=>?@[]^_{|}~`
- [ ] Test login and store cookie value
- [ ] Test that `/profile` contains the username `integration-test`
- [ ] Create a blog post and ensure that the database contains the blog post with the ID
- [ ] Edit the blog post above and ensure the database is updated
- [ ] Delete blog post and ensure that it's deleted from the database
- [ ] Ensure that editing a blog post that doesn't exist does not create it in the database
- [ ] For each of the previous "POST" requests, ensure that the function fails if the CSRF token is not provided

### Future Task Two

By this point, we should not be commiting code to the default branch directly. The master branch needs to implement two things. One is to ensure that each commit passes integration tests. If the integration test is succesful, we want to deploy first to something called PAT. The PAT environment should already be existing but have some sort of IP whitelisting and some sort of way to add and remove allowed IPs via an API. A backup of the database should be made before we run through the deployment test. Once deployed, the health check should be hit and expect to return a 204. Then the `/tag` should be hit to ensure that the expected application version is deployed. If the health check fails we need to fail the action. But first, try to rollback the app as well as restoring the database. Run the health check on the rolled back version of the code as well in case is more than one bug.
 
If the health check is successful, perform a rollback. Hit the health check again to ensure that a rollback in production would not fail. If it did fail, need to restore the database and fail the action.
 
Lastly, we can deploy to production with high confidence that if something that we didn't catch goes wrong, a roll back is always possible without needing to fix a bug ASAP.

When I say deploying to PAT and PROD, these are two seperate kubernetes clusters. Each kubernetes cluster will have a "blue" group and a "green" group which are determined using [kubernetes namespaces](https://www.reddit.com/r/kubernetes/comments/177laoe/comment/k4tolj5). Essentially, a deployment consists of  updating the clusters of the inactive namespace and then telling the kubernetes operator to direct traffic to the clusters that were updated.

When we have such thorough deployment testing, we can catch bugs related to making updates to our databases. We should rarely need to touch the database manually. The application code should have migration code to apply changes to the database in the corresponding environment. Typically, when a database modification is necessary, the first deployment is limited to modifying the database such that a rollback would not break. The second deployment consists of the code changes to utilized the new modifications of the database. When the old schema is deprecated, the backwards compatible parts of the database can be removed since they are redundant.

### Future Task Three

- [ ] Add the authorized editors column in such a way to avoid manually editing the production database. This is called a database migration.
- [ ] Impement logic related to authorized editors
- [ ] Update integration tests to reflect new features

### Future Task Four

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
        - [Password Storage - OWASP Cheat Sheet Series](https://cheatsheetseries.owasp.org/cheatsheets/Password_Storage_Cheat_Sheet.html)
        - [SecurityEssentials](https://github.com/johnstaveley/SecurityEssentials/blob/master/SecurityEssentials/App_Start/Startup.Auth.cs)

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
