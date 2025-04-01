-- Enable UUID generation
CREATE EXTENSION IF NOT EXISTS pgcrypto;

-- Comments table stores all user comments for articles
CREATE TABLE comments (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    content TEXT NOT NULL,
    article_id UUID NOT NULL, -- Reference to article in Articles service
    author_id UUID NOT NULL,  -- Reference to user in Auth service
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- Indexes for better query performance
CREATE INDEX idx_comments_article_id ON comments(article_id);
CREATE INDEX idx_comments_author_id ON comments(author_id);
CREATE INDEX idx_comments_created_at ON comments(created_at);
