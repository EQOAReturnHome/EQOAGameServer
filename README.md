# EQOAGameServer
The /current/ overall eqoa game server, not so complex, complex.

# TCPServerTests

EQOA Account server.
Utilizing TCP server in C#, able to process and interact with a few requests of the client.
- Primarily logging in (Authentication of username/password).
- Creating an account.
- Changing password.
- All other requests should be met with, "This option is disabled in order to bring you content faster. If you feel this is an error,please contact the development team for further assistance."

This is super basic at this time and just automatically processes and accepts these requests.
We have a database utilizing mariadb at this time, to be shared soon.

Functions needing to be added:
- Some SQL Choice (raw? orm?)
- Verifying username/password with sql.
- Authenticating old password and replacing it with new password if correct.
- Receiving create account requests, checking for username availability and writing the information to the database.

Client setup can be found here: http://wiki.eqoarevival.com/index.php/Client_Setup

For a windows environment, a DNS redirect can be found here: http://wiki.eqoarevival.com/index.php/Server_Setup_Windows#DNS_Server

For a windows environment, A HTTP Server can be found here: http://wiki.eqoarevival.com/index.php/Server_Setup_Windows#HTTP_Server

For Linux environments, A HTTP Server can be used through python's httpserve. Apache can also be used via a more traditional web server setup and seems to be more stable.

# EQOA-Proto-C-Sharp

UDP server implementing the packet framework to handle packet requests up to character select, including creating and deleting characters.
This server implements a keep alive which keeps the server select menu and Character select menu alive for a indefinite amount of time currently.


