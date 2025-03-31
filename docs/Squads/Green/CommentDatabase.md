# Comment Service Database Schema

## Comments Table

```sql
-- Comments table stores all user comments for articles
CREATE TABLE comments (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    content TEXT NOT NULL,
    articleId UUID NOT NULL, -- Reference to article in Articles service
    authorId UUID NOT NULL,  -- Reference to user in Auth service
    authorName VARCHAR(255) NOT NULL, -- Denormalized for display without querying User service
    createdAt TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updatedAt TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- Indexes for better query performance
CREATE INDEX idx_comments_articleId ON comments(articleId);
CREATE INDEX idx_comments_authorId ON comments(authorId);
CREATE INDEX idx_comments_createdAt ON comments(createdAt);
```

## Authors Table

```sql
-- Minimal authors table for the Comment service
-- Only stores essential author information for this service's operation
-- The authoritative source for author data remains in the User/Auth service
CREATE TABLE authors (
    id UUID PRIMARY KEY, -- Same ID as used in the Auth service
    name VARCHAR(255) NOT NULL

-- Index for faster lookups
CREATE INDEX idx_authors_name ON authors(name);
```

The Authors table allows the Comment service to:
1. Cache basic author information locally
2. Function independently if the User/Auth service is temporarily unavailable
3. Perform joins for queries without cross-service calls
