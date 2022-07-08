# ClientTripApp
REST Api application created using EntityFramework Core. 
Endpoints:
GET    /api/trips 
- Json list of trips in descending order after the start date of the trip
DELETE /api/clients/{idClient}
- Delete client
POST   /api/trips/{idTrip}/clients
- Assign a customer to the trip

