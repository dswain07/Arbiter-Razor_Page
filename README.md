# Arbiter

### University Research Publication & Peer-Review Hub

**Advancing Research. Connecting Scholars.**

Arbiter is an ASP.NET Core Razor Pages web application for organizing university research submissions, discovering scholarly work, maintaining author records, and managing peer reviews. Built as an independent graduate-level project for **CSCE 547 — Windows Programming**, it takes visual inspiration from the University of South Carolina through a garnet-accented, light/dark interface.

## Features

#### 1. Research Paper Management (CRUD)
- Create, read, update, and delete research paper submissions.
- Manage paper titles, abstracts, keywords, submission dates, statuses, and research tracks.
- View detailed information about individual research papers.
- Associate papers with academic research tracks.

#### 2. Author Management (CRUD)
- Create, view, edit, and delete author profiles.
- Maintain author information, including full name, email, academic affiliation, and ORCID ID.
- Store author information independently from research papers.

#### 3. Peer Review Management (CRUD)
- Create, view, update, and delete peer reviews.
- Associate reviews with research papers.
- Record reviewer names, evaluation scores, comments, and review dates.

#### 4. Advanced Research Paper Search
- Search research papers by Paper ID, Title, Abstract, and Keywords.
- Filter research papers by submission status and research track.
- Combine search criteria to narrow results.
- Implement database querying using Entity Framework Core.

#### 5. Database and Data Layer
- Implement a relational SQLite database containing five tables:
  - Papers
  - Authors
  - Reviews
  - Tracks
  - PaperAuthors
- Populate the database with 40 realistic demonstration records.
- Configure primary keys, foreign keys, and entity relationships using Entity Framework Core.
- Maintain a separate `Arbiter.Data` Class Library for database entities, DbContext, and migrations.
- Provide verified `DDL.sql` and `DML.sql` scripts for database reconstruction.

#### 6. Administration — Research Track Management
- Provide a dedicated Administration section for managing research tracks.
- Create, view, edit, and delete research tracks.
- Manage track descriptions and maximum submission quotas.
- Display assigned paper counts and available submission capacity.
- Prevent deletion of research tracks containing associated papers.

#### 7. User Settings and Theme Customization
- Provide a dedicated Settings page.
- Support Light and Dark themes using Bootstrap 5.3.
- Allow users to switch themes instantly without restarting the application.
- Preserve the selected theme across page navigation and browser sessions using `localStorage`.

---

### Additional Features — Beyond Assignment Requirements

The following enhancements were implemented to improve Arbiter's functionality, usability, security, and overall presentation.

#### 1. Administrator Authentication
- Implement password-protected administrator access using ASP.NET Core cookie authentication.
- Restrict access to Administration pages through role-based authorization.
- Store the administrator password outside the source code using .NET User Secrets.
- Provide a two-step logout process with confirmation.
- Configure HTTPS support for secure local development.

#### 2. Enhanced University-Inspired Homepage
- Design a professional homepage for **Arbiter — University Research Publication & Peer-Review Hub**.
- Incorporate University of South Carolina-inspired garnet, black, and white styling.
- Include a welcome banner, project introduction, and interactive feature cards.
- Provide quick navigation to research papers, paper submissions, authors, and peer reviews.

## Technology Stack

| Layer | Technology |
| --- | --- |
| Web UI | ASP.NET Core Razor Pages (.NET 10), Razor, HTML, CSS, JavaScript |
| Styling | Bootstrap 5.3.3 and Bootstrap Icons |
| Data access | Entity Framework Core 10 |
| Database | SQLite |
| Authentication | ASP.NET Core cookie authentication and authorization policies |
| Development | .NET SDK 10, Visual Studio Code, Git |

## Architecture

Arbiter separates the web application from its data-access class library:

```text
Arbiter/
├── src/
│   ├── Arbiter.Data/
│   │   ├── Context/             # EF Core DbContext
│   │   ├── Entities/            # Database entities
│   │   └── Migrations/          # EF Core migrations
│   └── Arbiter.Web/
│       ├── Data/                # Database initializer and seed data
│       ├── Pages/
│       │   ├── Admin/Tracks/    # Protected track management
│       │   ├── Authors/         # Author CRUD
│       │   ├── Papers/          # Paper CRUD and search
│       │   ├── Reviews/         # Review CRUD
│       │   ├── Settings/        # Theme settings
│       │   ├── AdminLogin.cshtml
│       │   ├── AdminLogout.cshtml
│       │   └── Shared/          # Shared layout
│       ├── wwwroot/            # CSS, JavaScript, and frontend assets
│       └── Program.cs          # Services, middleware, and startup
├── Scripts/
│   ├── DDL.sql                 # Application schema
│   └── DML.sql                 # Sample data
└── README.md
```

**Request flow:** Browser → Razor Page/PageModel (`Arbiter.Web`) → EF Core `ArbiterDbContext` (`Arbiter.Data`) → SQLite.

## Database Design

The database contains five application tables:

| Table | Purpose |
| --- | --- |
| `Tracks` | Research categories and maximum submission quotas |
| `Papers` | Research submissions, statuses, dates, and track associations |
| `Authors` | Researcher names, contact information, affiliations, and ORCID-style identifiers |
| `PaperAuthors` | Paper–author associations, author order, and corresponding-author designation |
| `Reviews` | Reviewer names, scores, comments, dates, and paper associations |

**Relationships:** One Track has many Papers; one Paper has many Reviews; Papers and Authors have a many-to-many relationship represented by `PaperAuthors`.

The demonstration dataset includes **40 records** across the five tables: 5 tracks, 8 authors, 10 papers, 10 paper–author associations, and 7 reviews. The SQLite DDL and DML scripts have been tested against a separate database, including a foreign-key integrity check.


## Running the Application

Arbiter is built using ASP.NET Core Razor Pages and .NET 10. It can be run on both **Windows** and **macOS** using Visual Studio, Visual Studio Code, or a terminal.

### Prerequisites

Before running Arbiter, make sure you have:

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Git](https://git-scm.com/downloads)
- A development environment:
  - **Windows:** Visual Studio 2022 or newer with .NET 10 support, or Visual Studio Code
  - **macOS:** Visual Studio Code
- An internet connection for restoring NuGet dependencies

> **Note:** SQLite and Entity Framework Core dependencies are managed through the project. You do not need to install a separate database server.

### 1. Clone the Repository

Open **PowerShell or Command Prompt (Windows)** or **Terminal (macOS)**.

Run:

```bash
git clone <YOUR-REPOSITORY-URL>
cd Arbiter
```

Replace `<YOUR-REPOSITORY-URL>` with the GitHub repository URL.

### 2. Restore and Build

From the Arbiter project directory, run:

```bash
dotnet restore
dotnet build
```

This downloads the required NuGet packages and builds the application.

A successful build confirms that the project is ready to run.

### 3. Configure the Administrator Password

Arbiter includes a password-protected Administration section.

Set up an administrator password using .NET User Secrets:

```bash
dotnet user-secrets init --project src/Arbiter.Web
dotnet user-secrets set "AdminAuth:Password" "YourPasswordHere" --project src/Arbiter.Web
```

Replace `YourPasswordHere` with a password of your choice.

> **Security:** Never commit your administrator password to GitHub. .NET User Secrets stores development credentials outside the project directory.

### 4. Enable HTTPS

Trust the ASP.NET Core development certificate:

```bash
dotnet dev-certs https --trust
```

**Windows:** Approve the certificate trust prompt if displayed.

**macOS:** Approve the certificate in your Keychain if prompted.

This enables secure HTTPS connections for local development.

### 5. Run Arbiter

From the repository root, execute:

```bash
dotnet run --project src/Arbiter.Web --launch-profile https
```

Once the application starts, open your browser and navigate to:

**https://localhost:7234**

If the application uses a different port, use the HTTPS address displayed in your terminal.

### 6. Explore the Application

After launching Arbiter, you can:

- **Home:** Explore the university research hub.
- **Papers:** Browse, search, create, edit, and delete research papers.
- **Authors:** View and manage researcher profiles.
- **Reviews:** Explore and manage peer reviews.
- **Administration:** Sign in using the administrator password to manage research tracks.
- **Settings:** Switch between Light and Dark themes.

The application uses SQLite with Entity Framework Core migrations and automatically initializes its database with demonstration records when started.

### Stopping the Application

To stop Arbiter, return to the terminal and press:

**Ctrl + C**

The application can be restarted using the same `dotnet run` command.


## SQL Scripts

- **`Scripts/DDL.sql`** creates the five application tables and their database constraints/indexes.
- **`Scripts/DML.sql`** inserts the demonstration dataset.

To reconstruct the application data in a **separate test database** using the SQLite CLI:

```bash
sqlite3 ArbiterRecreated.db < Scripts/DDL.sql
sqlite3 ArbiterRecreated.db < Scripts/DML.sql
sqlite3 ArbiterRecreated.db "PRAGMA foreign_key_check;"
```

An empty response from `PRAGMA foreign_key_check;` indicates no detected foreign-key violations. These standalone scripts intentionally omit EF Core's migration-history metadata; do not substitute the recreated test database for the application's migration-managed database without reconciling migration history.

## Project Scope and Future Improvements

Arbiter demonstrates a functional academic research workflow, but it is not yet a production publication system. Possible next steps include:

- Separate authenticated accounts and permissions for authors, reviewers, and administrators.
- File uploads for paper manuscripts and revised versions.
- Reviewer assignment, review deadlines, and decision workflows.
- Stronger data validation, pagination, and audit logging.
- Notifications and publication visibility rules.
- Accessibility testing, automated tests, and deployment hardening.

## Academic Project

Developed for **CSCE 547 — Windows Programming**, Minor Project 3 (graduate-level requirements), using ASP.NET Core Razor Pages and a separate data class library.

**Arbiter is an independent educational project inspired by the University of South Carolina research community.**


## Author

Divyashanu Swain