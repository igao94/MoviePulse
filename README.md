# 🎬 MoviePulse Web API
This is an ASP.NET Core Web API application, designed as a mini IMDb clone, enabling users to explore, rate, and manage movies. 
It's built with Clean Architecture principles, providing a robust and scalable solution for movie enthusiasts.

**The application is preconfigured with an In-Memory database for easy testing but can be seamlessly switched to SQL Server.**

## 📌 Features

✅ **Authentication and Authorization**

- Users must register and log in using ***JWT bearer tokens*** to access the application.
- Password ***hashing*** and ***salting*** are handled manually.
### User roles are manually defined:
- **Member:** Has limited application functionalities.
- **Admin:** Possesses elevated privileges and more functions.

## 🍿 Movie Management

- Users can search for movies.
- Users can rate movies and add them to a watchlist.
- Users can mark movies as watched.
- Admins can create and assign genres to movies.
- Users can also search for movies by genre.

## 🌟 Celebrity Profiles

- The system includes Celebrity profiles representing actors, producers, and similar roles.
- Celebrities can be linked to relevant movies.

## 🖼️ Cloudinary Integration for Media

- User profile pictures are stored using ***Cloudinary***.
- Movie posters are also managed and stored via ***Cloudinary***.

## 📊 Background Service for Average Ratings

- A background service is configured to update the average movie rating based on user ratings.
- For easier testing, this service updates the average rating every minute.

## 🗃️ In-Memory or SQL Server Database

- The app uses an ***In-Memory database*** by default for quick setup and testing.
- To use ***SQL Server***, set ***UseInMemoryDatabase*** to false in ***appsettings.Development.json*** for testing with a persistent database.

## 🧪 Postman Collection
- Postman collection is available with all API endpoints for easy testing.

## 🧑‍💻 Admin Credentials
- Email: admin@test.com
- Password: Pa$$w0rd

## 🚀 Technologies Used
- ASP.NET Core Web API
- Entity Framework Core
- Microsoft SQL Server / In-Memory Database
- MediatR
- AutoMapper
- FluentValidation
- Cloudinary
- JWT Bearer Authentication
- Cursor pagination
- Clean Architecture

## ⚙️ Getting Started

Clone the repository:
Bash

git clone https://github.com/igao94/MoviePulse
