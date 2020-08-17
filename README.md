# Acme Web API
 
ASP.NET Core Web API + Open API, cung cấp các hàm

**[GET] /api/Cinema/GetAvailableSeats**

Trả lại danh sách cách vùng ghế còn trống, có thể đặt chỗ

Tham số:

- seatsCount: int - Số lượng ghế cần đặt

**[POST] /api/Cinema/Reserve**

Đặt ghế

Tham số:

- seats: danh sách tọa độ các ghế muốn đặt

**[GET] /api/Cinema/PrintCinema**

In ra các ghế dưới dạng ma trận phục vụ mục đích test. Ghế có giá trị:

- 0: Có thể đặt

- 1: Đã được đặt

- -1: Còn trống nhưng không thể đặt vì phải giãn cách

![Screenshot](https://github.com/televisionpham/Teko/blob/master/Screenshot01.png)
