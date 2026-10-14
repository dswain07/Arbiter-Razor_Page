CREATE TABLE IF NOT EXISTS "Tracks" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Tracks" PRIMARY KEY AUTOINCREMENT,
    "Name" TEXT NOT NULL,
    "Description" TEXT NOT NULL,
    "MaxQuota" INTEGER NOT NULL
);
CREATE TABLE IF NOT EXISTS "Authors" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Authors" PRIMARY KEY AUTOINCREMENT,
    "FullName" TEXT NOT NULL,
    "Email" TEXT NOT NULL,
    "Affiliation" TEXT NOT NULL,
    "OrcidId" TEXT NOT NULL
);
CREATE TABLE IF NOT EXISTS "Papers" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Papers" PRIMARY KEY AUTOINCREMENT,
    "Abstract" TEXT NOT NULL,
    "Keywords" TEXT NULL,
    "LastModifiedDate" TEXT NULL,
    "Status" TEXT NOT NULL,
    "SubmissionDate" TEXT NOT NULL,
    "Title" TEXT NOT NULL,
    "TrackId" INTEGER NOT NULL,
    CONSTRAINT "FK_Papers_Tracks_TrackId" FOREIGN KEY ("TrackId") REFERENCES "Tracks" ("Id") ON DELETE CASCADE
);
CREATE INDEX "IX_Papers_TrackId" ON "Papers" ("TrackId");
CREATE TABLE IF NOT EXISTS "PaperAuthors" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_PaperAuthors" PRIMARY KEY AUTOINCREMENT,
    "AuthorId" INTEGER NOT NULL,
    "AuthorOrder" INTEGER NOT NULL,
    "IsCorrespondingAuthor" INTEGER NOT NULL,
    "PaperId" INTEGER NOT NULL,
    CONSTRAINT "FK_PaperAuthors_Authors_AuthorId" FOREIGN KEY ("AuthorId") REFERENCES "Authors" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_PaperAuthors_Papers_PaperId" FOREIGN KEY ("PaperId") REFERENCES "Papers" ("Id") ON DELETE CASCADE
);
CREATE INDEX "IX_PaperAuthors_AuthorId" ON "PaperAuthors" ("AuthorId");
CREATE INDEX "IX_PaperAuthors_PaperId" ON "PaperAuthors" ("PaperId");
CREATE TABLE IF NOT EXISTS "Reviews" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_Reviews" PRIMARY KEY AUTOINCREMENT,
    "Comments" TEXT NOT NULL,
    "PaperId" INTEGER NOT NULL,
    "ReviewDate" TEXT NOT NULL,
    "ReviewerName" TEXT NOT NULL,
    "Score" INTEGER NOT NULL,
    CONSTRAINT "FK_Reviews_Papers_PaperId" FOREIGN KEY ("PaperId") REFERENCES "Papers" ("Id") ON DELETE CASCADE
);
CREATE INDEX "IX_Reviews_PaperId" ON "Reviews" ("PaperId");
