# Comment Service Database Schema

## Comments Table

```sql
-- Comments table stores all user comments for articles
CREATE TABLE comments (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    content TEXT NOT NULL,
    articleId UUID NOT NULL, -- Reference to article in Articles service
    authorId UUID NOT NULL,  -- Reference to user in Auth service
    authorName VARCHAR(255) NULL, -- Denormalized for display without querying User service
    createdAt TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updatedAt TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- Indexes for better query performance
CREATE INDEX idx_comments_articleId ON comments(articleId);
CREATE INDEX idx_comments_authorId ON comments(authorId);
CREATE INDEX idx_comments_createdAt ON comments(createdAt);
```

