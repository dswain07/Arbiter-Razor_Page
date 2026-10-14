using Arbiter.Data.Context;
using Arbiter.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arbiter.Web.Data;

public static class DbInitializer
{
    public static void Initialize(ArbiterDbContext context)
    {
        // Apply any pending migrations.
        context.Database.Migrate();

        SeedTracks(context);

        SeedAuthors(context);

        SeedPapers(context);

        SeedPaperAuthors(context);

        SeedReviews(context);
    }

        
private static void SeedAuthors(ArbiterDbContext context)
{
    var authors = new[]
    {
        new Author
        {
            FullName = "Emily Carter",
            Email = "emily.carter@example.com",
            Affiliation = "Stanford University",
            OrcidId = "0000-0000-0000-0001"
        },
        new Author
        {
            FullName = "Michael Chen",
            Email = "michael.chen@example.com",
            Affiliation = "Massachusetts Institute of Technology",
            OrcidId = "0000-0000-0000-0002"
        },
        new Author
        {
            FullName = "Sarah Johnson",
            Email = "sarah.johnson@example.com",
            Affiliation = "University of California, Berkeley",
            OrcidId = "0000-0000-0000-0003"
        },
        new Author
        {
            FullName = "David Wilson",
            Email = "david.wilson@example.com",
            Affiliation = "Carnegie Mellon University",
            OrcidId = "0000-0000-0000-0004"
        },
        new Author
        {
            FullName = "Olivia Martinez",
            Email = "olivia.martinez@example.com",
            Affiliation = "Georgia Institute of Technology",
            OrcidId = "0000-0000-0000-0005"
        },
        new Author
        {
            FullName = "James Anderson",
            Email = "james.anderson@example.com",
            Affiliation = "University of Michigan",
            OrcidId = "0000-0000-0000-0006"
        },
        new Author
        {
            FullName = "Sophia Brown",
            Email = "sophia.brown@example.com",
            Affiliation = "University of Washington",
            OrcidId = "0000-0000-0000-0007"
        },
        new Author
        {
            FullName = "Daniel Lee",
            Email = "daniel.lee@example.com",
            Affiliation = "University of Texas at Austin",
            OrcidId = "0000-0000-0000-0008"
        }
    };

    foreach (var author in authors)
    {
        bool exists = context.Authors
            .Any(a => a.Email == author.Email);

        if (!exists)
        {
            context.Authors.Add(author);
        }
    }

    context.SaveChanges();
}


private static void SeedPapers(ArbiterDbContext context)
{
    var tracks = context.Tracks
        .ToDictionary(t => t.Name, t => t.Id);

    var papers = new[]
    {
        new Paper
        {
            Title = "Explainable Deep Learning for Medical Diagnosis",
            Abstract = "This study investigates explainable artificial intelligence techniques for improving the interpretability of deep learning models used in medical image classification.",
            Keywords = "Explainable AI, Deep Learning, Healthcare",
            SubmissionDate = new DateTime(2026, 9, 2),
            Status = "Accepted",
            TrackId = tracks["Artificial Intelligence"]
        },
        new Paper
        {
            Title = "Federated Learning for Privacy-Preserving Healthcare",
            Abstract = "This research examines federated learning architectures that enable collaborative machine learning across healthcare institutions without sharing sensitive patient data.",
            Keywords = "Federated Learning, Privacy, Healthcare",
            SubmissionDate = new DateTime(2026, 9, 5),
            Status = "Under Review",
            TrackId = tracks["Artificial Intelligence"]
        },
        new Paper
        {
            Title = "Zero-Trust Security in Cloud Infrastructure",
            Abstract = "This paper evaluates zero-trust security architectures for protecting distributed cloud environments against unauthorized access and lateral movement.",
            Keywords = "Zero Trust, Cloud Security, Cybersecurity",
            SubmissionDate = new DateTime(2026, 9, 8),
            Status = "Accepted",
            TrackId = tracks["Cybersecurity"]
        },
        new Paper
        {
            Title = "Machine Learning Approaches to Intrusion Detection",
            Abstract = "This study compares supervised machine learning algorithms for detecting malicious network activity using network traffic features.",
            Keywords = "Intrusion Detection, Machine Learning, Network Security",
            SubmissionDate = new DateTime(2026, 9, 10),
            Status = "Under Review",
            TrackId = tracks["Cybersecurity"]
        },
        new Paper
        {
            Title = "Optimizing Distributed Database Transactions",
            Abstract = "This research explores transaction scheduling and concurrency control techniques to improve throughput and consistency in distributed database systems.",
            Keywords = "Distributed Systems, Databases, Transactions",
            SubmissionDate = new DateTime(2026, 9, 12),
            Status = "Submitted",
            TrackId = tracks["Systems"]
        },
        new Paper
        {
            Title = "Energy-Efficient Scheduling in Cloud Computing",
            Abstract = "This paper proposes workload scheduling strategies that reduce energy consumption while maintaining performance in cloud computing environments.",
            Keywords = "Cloud Computing, Scheduling, Energy Efficiency",
            SubmissionDate = new DateTime(2026, 9, 15),
            Status = "Accepted",
            TrackId = tracks["Systems"]
        },
        new Paper
        {
            Title = "Predictive Analytics for Urban Transportation",
            Abstract = "This study applies statistical learning and time-series forecasting to predict traffic congestion and improve urban transportation planning.",
            Keywords = "Predictive Analytics, Transportation, Time Series",
            SubmissionDate = new DateTime(2026, 9, 18),
            Status = "Under Review",
            TrackId = tracks["Data Science"]
        },
        new Paper
        {
            Title = "Detecting Financial Fraud Using Graph Neural Networks",
            Abstract = "This research investigates graph neural network models for identifying suspicious transaction patterns in financial networks.",
            Keywords = "Graph Neural Networks, Fraud Detection, Data Science",
            SubmissionDate = new DateTime(2026, 9, 20),
            Status = "Rejected",
            TrackId = tracks["Data Science"]
        },
        new Paper
        {
            Title = "Automated Software Testing Using Large Language Models",
            Abstract = "This paper explores the use of large language models to automatically generate unit tests and identify potential software defects.",
            Keywords = "Software Testing, Large Language Models, Automation",
            SubmissionDate = new DateTime(2026, 9, 22),
            Status = "Accepted",
            TrackId = tracks["Software Engineering"]
        },
        new Paper
        {
            Title = "Improving Code Review Through Static Analysis",
            Abstract = "This study evaluates static analysis techniques for identifying maintainability issues and security vulnerabilities during software code review.",
            Keywords = "Static Analysis, Code Review, Software Quality",
            SubmissionDate = new DateTime(2026, 9, 25),
            Status = "Submitted",
            TrackId = tracks["Software Engineering"]
        }
    };

    foreach (var paper in papers)
    {
        bool exists = context.Papers
            .Any(p => p.Title == paper.Title);

        if (!exists)
        {
            context.Papers.Add(paper);
        }
    }

    context.SaveChanges();
}


private static void SeedPaperAuthors(ArbiterDbContext context)
{
    var relationships = new[]
    {
        (
            Title: "Explainable Deep Learning for Medical Diagnosis",
            Email: "emily.carter@example.com"
        ),
        (
            Title: "Federated Learning for Privacy-Preserving Healthcare",
            Email: "michael.chen@example.com"
        ),
        (
            Title: "Zero-Trust Security in Cloud Infrastructure",
            Email: "sarah.johnson@example.com"
        ),
        (
            Title: "Machine Learning Approaches to Intrusion Detection",
            Email: "david.wilson@example.com"
        ),
        (
            Title: "Optimizing Distributed Database Transactions",
            Email: "olivia.martinez@example.com"
        ),
        (
            Title: "Energy-Efficient Scheduling in Cloud Computing",
            Email: "james.anderson@example.com"
        ),
        (
            Title: "Predictive Analytics for Urban Transportation",
            Email: "sophia.brown@example.com"
        ),
        (
            Title: "Detecting Financial Fraud Using Graph Neural Networks",
            Email: "daniel.lee@example.com"
        ),
        (
            Title: "Automated Software Testing Using Large Language Models",
            Email: "emily.carter@example.com"
        ),
        (
            Title: "Improving Code Review Through Static Analysis",
            Email: "michael.chen@example.com"
        )
    };

    foreach (var relationship in relationships)
    {
        var paper = context.Papers
            .Single(p => p.Title == relationship.Title);

        var author = context.Authors
            .Single(a => a.Email == relationship.Email);

        bool exists = context.PaperAuthors.Any(pa =>
            pa.PaperId == paper.Id &&
            pa.AuthorId == author.Id);

        if (!exists)
        {
            context.PaperAuthors.Add(new PaperAuthor
            {
                PaperId = paper.Id,
                AuthorId = author.Id,
                AuthorOrder = 1,
                IsCorrespondingAuthor = true
            });
        }
    }

    context.SaveChanges();
}


private static void SeedReviews(ArbiterDbContext context)
{
    var reviews = new[]
    {
        new
        {
            PaperTitle = "Explainable Deep Learning for Medical Diagnosis",
            Reviewer = "Dr. Robert Miller",
            Score = 5,
            Comments = "Excellent contribution to explainable AI. The methodology is well structured and the experimental results are convincing.",
            Date = new DateTime(2026, 10, 1)
        },
        new
        {
            PaperTitle = "Federated Learning for Privacy-Preserving Healthcare",
            Reviewer = "Dr. Jennifer Adams",
            Score = 4,
            Comments = "Strong motivation and promising results. Additional evaluation across heterogeneous healthcare datasets would strengthen the paper.",
            Date = new DateTime(2026, 10, 3)
        },
        new
        {
            PaperTitle = "Zero-Trust Security in Cloud Infrastructure",
            Reviewer = "Dr. Christopher Davis",
            Score = 5,
            Comments = "A thorough analysis of zero-trust architectures with practical recommendations for cloud infrastructure security.",
            Date = new DateTime(2026, 10, 4)
        },
        new
        {
            PaperTitle = "Machine Learning Approaches to Intrusion Detection",
            Reviewer = "Dr. Amanda Wilson",
            Score = 4,
            Comments = "The comparison of intrusion detection models is useful. More discussion of false positive rates is recommended.",
            Date = new DateTime(2026, 10, 5)
        },
        new
        {
            PaperTitle = "Energy-Efficient Scheduling in Cloud Computing",
            Reviewer = "Dr. Thomas Anderson",
            Score = 5,
            Comments = "The proposed scheduling approach demonstrates meaningful energy savings while maintaining system performance.",
            Date = new DateTime(2026, 10, 6)
        },
        new
        {
            PaperTitle = "Detecting Financial Fraud Using Graph Neural Networks",
            Reviewer = "Dr. Rebecca Taylor",
            Score = 2,
            Comments = "The research problem is relevant, but the experimental evaluation lacks sufficient baseline comparisons and statistical analysis.",
            Date = new DateTime(2026, 10, 7)
        },
        new
        {
            PaperTitle = "Automated Software Testing Using Large Language Models",
            Reviewer = "Dr. William Harris",
            Score = 5,
            Comments = "An innovative application of large language models to automated software testing, supported by clear experimental findings.",
            Date = new DateTime(2026, 10, 8)
        }
    };

    foreach (var item in reviews)
    {
        var paper = context.Papers
            .Single(p => p.Title == item.PaperTitle);

        bool exists = context.Reviews.Any(r =>
            r.PaperId == paper.Id &&
            r.ReviewerName == item.Reviewer);

        if (!exists)
        {
            context.Reviews.Add(new Review
            {
                PaperId = paper.Id,
                ReviewerName = item.Reviewer,
                Score = item.Score,
                Comments = item.Comments,
                ReviewDate = item.Date
            });
        }
    }

    context.SaveChanges();
}



    private static void SeedTracks(ArbiterDbContext context)
    {
        var tracks = new[]
        {
            new Track
            {
                Name = "Artificial Intelligence",
                Description = "AI and Machine Learning",
                MaxQuota = 100
            },
            new Track
            {
                Name = "Systems",
                Description = "Operating Systems",
                MaxQuota = 80
            },
            new Track
            {
                Name = "Cybersecurity",
                Description = "Security and Privacy",
                MaxQuota = 75
            },
            new Track
            {
                Name = "Data Science",
                Description = "Analytics and Data Engineering",
                MaxQuota = 90
            },
            new Track
            {
                Name = "Software Engineering",
                Description = "Software Design",
                MaxQuota = 120
            }
        };

        foreach (var track in tracks)
        {
            bool exists = context.Tracks
                .Any(t => t.Name == track.Name);

            if (!exists)
            {
                context.Tracks.Add(track);
            }
        }

        context.SaveChanges();
    }
}