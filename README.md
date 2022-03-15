## FeedbackReporting API

### Introduction
This project is a simple implementation of a feedback API. Meant to be used in an application where you want your users to submit some bug report or simply open tickets.
This was done as a job interview exercice and was meant to show off what I know. Meaning for instance that there is only a couple of unit tests mostly to show off that I know how tu use Moq and Xunit. But no whole code coverage.

### How to start the project
You can obviously clone the repository and launch it from your IDE in debug mode. This project feature a Swagger middleware for Development environment.

You can also use docker to build and run this. Simply execute the following commands on the repository root folder (see startup.sh script if you are on a Linux system) :

```
docker build -t feedbackreporting .
docker run -it --rm -p 8000:80 --name feedbackreporting_app feedbackreporting
```

If you are running from your IDE you can open https://localhost:44386/swagger/index.html to access Swagger.
If you choose Docker you can open https://0.0.0.0:8000/swagger/index.html to access Swagger.

The first thing you will want to do is login an user. 

By default two users of the two available roles already exists at startup : an 'Admin' role (userName 'admin', password 'admin') and a simple 'User' role (userName 'user', password 'user'). You can create more in the '/user' entry points.

![Admin login](/ReadmeImages/login_admin.PNG?raw=true "Admin login exemple")

After you had login yourself you will be provided an access token.
This token can be used in swagger using the 'Authorize' button to register the access token for further requests. This access token last for an hour before needing to re-log your user. 
Do not forget to add "Bearer" before copy pasting your access token.

![Access Token](/ReadmeImages/set_access_token_swagger.PNG?raw=true "Authorization header")

Then go on and create your first feedback entry. The create feedback entry point will return the id of the entry inserted. Only feedback creation and attachments upload is available to simples 'User', the remaining (user creation, search, etc) is only available to 'Admins'.

You can also attach documents to your feedback entry. The search entry point provide a keyword based search to avoid 'LIKE %' SQL requests in database. Keep in mind that this search is only performed on feedback description and on words that are more than 3 characters long. 
For thoses who will open source code the keyword mechanisme is more to trigger discussion than a real implementation (for instance the GetHashCode method is not safe from collisions).

### Features
- In memory sqlite database generator / connection factory / plus a BaseRepository implementation with UnitOfWork pattern
- Use cases logic for dependency injection and easy testing
- Repository design for database to let UnitOfWork design pattern handle connection / transactions
- The UseCaseBase abstract that wrap arround use cases calls to make sure all use case's database calls are within one rollbackable (in case of an error) transaction. By design it is still possible to make unitary database calls (in a service rather than a use case for instance)
- Unit tests with XUnit / Moq and a ressources builder example, easy to expand
- JWT identification for users with token expiration, user passwords are saved hashed
- Swagger middleware with authorization header compatibility for fast development environment testing