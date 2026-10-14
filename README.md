# Arbiter

### University Research Publication & Peer-Review Hub

**Advancing Research. Connecting Scholars.**

Arbiter is an ASP.NET Core Razor Pages web application for organizing university research submissions, discovering scholarly work, maintaining author records, and managing peer reviews. Built as an independent graduate-level project for **CSCE 547 — Windows Programming**, it takes visual inspiration from the University of South Carolina through a garnet-accented, light/dark interface.

> **Academic project disclaimer:** Arbiter is a student-developed demonstration application. It is not an official University of South Carolina service, publication repository, or endorsed platform. The included researchers, papers, and reviews are synthetic demonstration data.

## Features

- **Research papers:** Create, view, edit, and delete submissions; manage titles, abstracts, keywords, status, submission dates, and research tracks.
- **Advanced discovery:** Search papers by ID, title, abstract, or keywords; combine searches with status and track filters.
- **Authors:** Maintain author profiles, including affiliation, email, and ORCID-style demonstration identifiers.
- **Peer reviews:** Create and manage scored reviews with comments, review dates, and associated papers.
- **Track administration:** Manage research tracks, descriptions, and submission quotas; view assigned-paper counts and remaining capacity. Deletion of tracks with associated papers is blocked.
- **Administrator access:** Password-only administrator login with ASP.NET Core cookie authentication, role-based authorization for `/Admin`, and a two-step logout confirmation.
- **Personalization:** Switch between light and dark themes without restarting the application; the preference is saved in browser `localStorage`.
- **Enhanced homepage:** University-inspired garnet styling, project introduction, and quick links to the primary workflows.

## Technology Stack

| Layer | Technology |
| --- | --- |
| Web UI | ASP.NET Core Razor Pages (.NET 10), Razor, HTML, CSS, JavaScript |
| Styling | Bootstrap 5.3.3 and Bootstrap Icons |
| Data access | Entity Framework Core 10 |
| Database | SQLite |
| Authentication | ASP.NET Core cookie authentication and authorization policies |
| Development | .NET SDK 10, Visual Studio Code or Visual Studio, Git |

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

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Git
- A code editor such as Visual Studio Code or Visual Studio
- Optional: `dotnet-ef` CLI for migration management and `sqlite3` for inspecting/exporting the database

### 1. Clone and build

```bash
git clone <YOUR-REPOSITORY-URL>
cd Arbiter
dotnet restore
dotnet build
```

Replace `<YOUR-REPOSITORY-URL>` with the repository's actual GitHub clone URL.

### 2. Configure the database

The application uses SQLite and EF Core migrations. Set `ConnectionStrings:DefaultConnection` in `src/Arbiter.Web/appsettings.json` or via environment-specific configuration to point to a writable SQLite database, for example:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=Arbiter.db"
  }
}
```

The startup initializer applies EF Core migrations and seeds demonstration records if they do not already exist. Avoid committing local database files containing personal or sensitive information.

### 3. Configure the administrator password (development)

From the repository root:

```bash
dotnet user-secrets init --project src/Arbiter.Web
dotnet user-secrets set "AdminAuth:Password" "<CHOOSE-A-STRONG-PASSWORD>" --project src/Arbiter.Web
```

Replace the placeholder with a strong password. **Never commit the password or share it in screenshots.** User Secrets are intended for local development, not production secret management.

### 4. Trust the local HTTPS certificate

```bash
dotnet dev-certs https --trust
```

### 5. Run the application

```bash
dotnet run --project src/Arbiter.Web --launch-profile https
```

With the provided development launch settings, visit **https://localhost:7234**. If your environment uses a different port, use the address printed in the terminal.

> **Note:** The included authentication flow is a classroom-oriented, single-administrator implementation. For deployment, add production-grade identity management, credential storage, login-attempt protection, secure HTTPS hosting, and authorization rules for other editing workflows.

## Using Arbiter

| Page | Purpose |
| --- | --- |
| `/` | Welcome page and feature navigation |
| `/Papers` | Browse, search, filter, and manage research papers |
| `/Authors` | View and manage researchers |
| `/Reviews` | View and manage peer reviews |
| `/Settings` | Switch between light and dark appearance |
| `/AdminLogin` | Sign in as the administrator |
| `/Admin/Tracks` | Protected research-track management |
| `/AdminLogout` | Confirm administrator logout |

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

## Screenshots

Add screenshots captured from the running application before final submission:

| Screen | Screenshot |
| --- | --- |
| Enhanced homepage | _Add screenshot_ |
| Paper search and filters | _Add screenshot_ |
| Create, edit, and delete paper | _Add screenshots_ |
| Author and review management | _Add screenshots_ |
| Track administration | _Add screenshot_ |
| Light and dark settings | _Add screenshots_ |

For CSCE 547 submission, the required search, insert, update, and delete screenshots should also appear in the accompanying PDF or DOCX documentation.

## Project Scope and Future Improvements

Arbiter demonstrates a functional academic research workflow, but it is not yet a production publication system. Possible next steps include:

- Separate authenticated accounts and permissions for authors, reviewers, and administrators.
- File uploads for paper manuscripts and revised versions.
- Reviewer assignment, review deadlines, and decision workflows.
- Stronger data validation, pagination, and audit logging.
- Notifications and publication visibility rules.
- Accessibility testing, automated tests, and deployment hardening.

## Academic Context

Developed for **CSCE 547 — Windows Programming**, Minor Project 3 (graduate-level requirements), using ASP.NET Core Razor Pages and a separate data class library.

**Arbiter is an independent educational project inspired by the University of South Carolina research community; it is not affiliated with or endorsed by the university.**
