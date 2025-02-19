# **📚 Readly - A .NET 9 RESTful API for Book Management**

**Readly** is a modern **.NET 9 RESTful API** designed to manage books and authors efficiently. It includes robust *
*user authentication, book search, and a recommendation system** to enhance the reading experience.

### **🚀 Features:**

✅ **User Authentication & Authorization** (JWT-based)  
✅ **Manage Books & Authors** (CRUD operations)  
✅ **Advanced Book Search** (Title, Author, Genre, etc.)  
✅ **Personalized Recommendations** (AI-driven suggestions)  
✅ **Pagination & Filtering**  
✅ **OpenAPI Documentation**

### **🛠️ Tech Stack:**

- **.NET 9 Web API**
- **Entity Framework Core + PostgreSQL**
- **ASP.NET Identity for Authentication**
- **Minimal APIs**
- **Docker & Kubernetes Ready**

### **🛠️ Migrations:**

Create Migration / Add Migration 

Run Migration from src/ folder

    dotnet ef migrations add "InitialMigration" --context ReadlyDbContext --project Readly.Infrastructure --startup-project Readly.API --output-dir Data/Migrations
    dotnet ef database update --context ReadlyDbContext --project Readly.Infrastructure --startup-project Readly.API

### **📝 Requirements:**

---

#### **1. API Endpoints:**

**Books Endpoints:**

---

- **GET /books**: Retrieve a list of all books.
- **GET /books/:id**: Retrieve a specific book by its ID.
- **POST /books**: Create a new book. *(Protected: requires user authentication via JWT)*
- **PUT /books/:id**: Update an existing book by its ID. *(Protected: requires user authentication via JWT)*
- **DELETE /books/:id**: Delete a book by its ID. *(Protected: requires user authentication via JWT)*

**Authors Endpoints:**

- **GET /authors**: Retrieve a list of all authors.
- **GET /authors/:id**: Retrieve a specific author by their ID.
- **POST /authors**: Create a new author. *(Protected: requires user authentication via JWT)*
- **PUT /authors/:id**: Update an existing author by their ID. *(Protected: requires user authentication via JWT)*
- **DELETE /authors/:id**: Delete an author by their ID. *(Protected: requires user authentication via JWT)*

---

#### **2. Authentication:**

- **JWT Authentication**: Use **JSON Web Tokens (JWT)** for user authentication across all protected endpoints.
- **Registration Endpoint**:
    - **POST /register**: Allow users to register by providing necessary details (e.g., email, password, etc.).
- **Login Endpoint**:
    - **POST /login**: Allow users to login by providing credentials (e.g., email, password), and return a JWT token for
      subsequent requests.
- **Protected Endpoints**:
    - Endpoints for **POST /books**, **PUT /books/:id**, **DELETE /books/:id**, **POST /authors**, **PUT /authors/:id**,
      and **DELETE /authors/:id** should be protected and require the user to be authenticated with a valid JWT token.

---

#### **3. Database Schema:**

- Design a **relational database schema** with the following tables:
    - **Books**: A table to store information about books, including title, description, author_id (foreign key), etc.
    - **Authors**: A table to store information about authors, including name, biography, etc.
    - **Users**: A table to store user data for authentication (e.g., email, password hash, etc.).
    - **Favorites**: A table to store the user's favorite books, linking users and books (user_id, book_id).
    - Additional tables/models may be added as needed to fulfill application functionality.

---

#### **4. Search Functionality:**

- **GET /books?search=query**: Implement search functionality to find books by their title or the author's name.
    - The search query should return relevant results based on the query string, matching titles and author names.

---

#### **5. Recommendation System:**

- **User Favorites**:
    - Users can add books to their **favorites list** (a max of 20 books).
    - Users can **remove books** from their favorites list.

- **Suggested Books**:
    - Upon adding a book to their favorites, the system should recommend **5 books** to the user.
    - Recommendations should be based on **similarity** to the entire user's current favorites list. This could be done
      using a **similarity algorithm** (e.g., collaborative filtering, content-based filtering, etc.).
    - Each new book added to the user's favorites should update the recommendations, reflecting the full list of
      favorites.

- **Performance**:
    - The endpoint to get favorite books should return the response in **less than 1 second**.
    - Ensure that the recommendation system can handle real-time updates efficiently.

---

### **Technical Notes:**

- **Authentication & Authorization**:
    - Use JWT tokens for securing API endpoints.
    - Implement middleware to validate JWT tokens on protected routes.

- **Database**:
    - Use an appropriate **relational database** (e.g., SQL Server, PostgreSQL) for storing data.
    - Implement appropriate **indexes** for fast searching and recommendations.

- **Search**:
    - Implement **case-insensitive** searches and support partial matching for titles and author names.

- **Recommendation Algorithm**:
    - Consider using algorithms such as **content-based filtering** or **collaborative filtering** for suggesting
      similar books.
    - Optimize for performance to ensure that recommendations are returned in under 1 second.

---

### **🛠 Developer ReadMe**

Since we used CPM Central Package Management so use this setting for all CSPROJ files

    <PropertyGroup>
        <ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>
    </PropertyGroup>

### References

https://github.com/ardalis/CleanArchitecture/tree/main/src/Clean.Architecture.UseCases/Contributors
https://github.com/jasontaylordev/CleanArchitecture/blob/8f5a963d4b4af3c199061ca3eb50d6a919156ae6/src/Web/GlobalUsings.cs
https://github.com/dotnet/eShop/tree/main