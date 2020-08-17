# Acme Web API
 
ASP.NET Core Web API + Open API, provides the following functions:

**[GET] /api/Cinema/GetAvailableSeats**

Get available seats regions

Parameter:

- seatsCount: int - Number of seats to be reserved

**[POST] /api/Cinema/Reserve**

Reserve seats

Tham số:

- seats: List of seats coordinates

**[GET] /api/Cinema/PrintCinema**

Print seats as a 2D matrix (for testing purpose). Seat status:

- 0: Available

- 1: Reserved

- -1: Unavailable

**[POST] /api/Cinema/api/Cinema/Clear**

Reset all seats status to 0

**[POST] /api/Cinema/api/Cinema/FreeSeats**

Cancel reserved seats

Parameter:

- seats: List of seats to be cancelled

![Screenshot](https://github.com/televisionpham/Teko/blob/master/Screenshot01.png)
