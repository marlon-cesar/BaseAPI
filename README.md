# BaseAPI

A simple and extensible **.NET 7 Web API** project that demonstrates **Bearer token authentication**, external API integration, data normalization, and filtering with pagination.

## 📌 Features

- ✅ Bearer Token Authentication using JWT
- 🔁 Integration with external **Pokémon API**
- 🧩 Normalization and storage of Pokémon data into a local database
- 🔎 Endpoint for paginated and filtered Pokémon listing
- 📦 Built with clean and modular code structure

## ⚙️ Technologies Used

- .NET 7
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL (or other supported relational databases)
- JWT (JSON Web Token)
- Swagger (OpenAPI)



## 🧠 Pokémon Integration
A background service (or a manual endpoint) fetches data from a public Pokémon API, normalizes the information (name, types, stats, etc.), and saves it to the local database.

## 🔍 Pokémon Search with Filters and Pagination
Allows filtered search for Pokémon using query parameters.
