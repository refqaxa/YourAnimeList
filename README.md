# YourAnimeList CRUD Application

This ASP.NET Core MVC application allows users to manage a list of anime, perform CRUD operations, and maintain user-specific anime lists. The app supports authentication, authorization, and role-based access control.

---
## **Features**
- **CRUD Operations**: Create, Read, Update, and Delete anime entries.
- **User Authentication**: Register, login, and manage profiles.
- **Authorization**: Only anime creators or admins can edit or delete entries.
- **Role Management**: Admins have elevated permissions.
- **Navigation Properties**: Supports many-to-many relationships between users and anime.

---
## **Technologies Used**
- **Framework**: ASP.NET Core MVC
- **Database**: Microsoft SQL Server (EF Core)
- **Authentication & Authorization**: Identity Framework
- **Frontend**: Bootstrap 5, HTML, CSS

---
## **Setup Instructions**
### Prerequisites
- Visual Studio 2022 or newer
- .NET 6 SDK or later
- Microsoft SQL Server

### Installation Steps
1. Clone the repository:
   ```shell
   git clone https://github.com/your-repo/YourAnimeList.git
   ```
2. Open the project in Visual Studio.
3. Update `appsettings.json` with your SQL Server connection string:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=YourAnimeList;Trusted_Connection=True;"
   }
   ```
4. Apply database migrations:
   ```shell
   dotnet ef database update
   ```
5. Run the application:
   ```shell
   dotnet run
   ```

---
## **Entity Model Overview**
### Anime.cs
```csharp
public class Anime
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Episodes { get; set; }
    public DateTime Aired { get; set; }
    public string AddedBy { get; set; } = string.Empty;
    public List<UserAnimeList>? UserAnimeLists { get; set; }
}
```
### UserAnimeList.cs
```csharp
public class UserAnimeList
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public Guid AnimeId { get; set; }
    public Anime Anime { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
}
```

---
## **Key Functionality Highlights**
### Add Anime
- Form submission creates a new anime entry.
- User authentication required.

### Edit Anime
- Only the creator or an admin can edit entries.
- Navigation property `UserAnimeLists` made nullable to avoid unwanted validation issues.

### Delete Anime
- Authorized users can delete anime.
- Cascade deletion ensures related data removal.

### List & Search Anime
- Users can view and search anime by name or description.

---
## **Authorization Rules**
- **Authenticated Users**: Can create and add anime to their list.
- **Anime Creators**: Can edit and delete their own anime.
- **Admins**: Have full control over all anime entries.

---
## **Database Migration Commands**
- **Add Migration**:
  ```shell
  dotnet ef migrations add MigrationName
  ```
- **Update Database**:
  ```shell
  dotnet ef database update
  ```

---
## **Project Structure Overview**
```
YourAnimeList
│── Controllers
│   └── AnimeController.cs
│── Models
│   ├── Anime.cs
│   └── UserAnimeList.cs
│── Views
│   ├── Anime
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   │   └── Details.cshtml
│── Data
│   └── ApplicationDbContext.cs
│── Program.cs
│── Startup.cs
└── appsettings.json
```

---
## **License**
This project is open-source and available under the MIT License.

---
## **Contributors**
- Your Name (Project Owner)

---
## **Contact**
For issues, suggestions, or contributions, please open an issue or create a pull request on GitHub.

