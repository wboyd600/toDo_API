CREATE DATABASE todos_app;

CREATE TABLE IF NOT EXISTS Todos (
    id uuid PRIMARY KEY DEFAULT gen_random_uuid() NOT NULL,
    userID uuid NOT NULL,
    title TEXT NOT NULL,
    created timestamptz NOT NULL,
    due timestamptz NOT NULL,
    completed BOOLEAN NOT NULL,
    FOREIGN KEY (userID)
)

CREATE TABLE IF NOT EXISTS Users (
    id uuid PRIMARY KEY DEFAULT gen_random_uuid() NOT NULL,
    username TEXT NOT NULL,
    password TEXT NOT NULL,
    FOREIGN KEY (username)
)