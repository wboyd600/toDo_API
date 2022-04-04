CREATE DATABASE todos_app;


-- CREATE TABLE IF NOT EXISTS users (
--     id uuid PRIMARY KEY DEFAULT gen_random_uuid() NOT NULL,
--     username TEXT NOT NULL,
--     password TEXT NOT NULL,
--     salt TEXT NOT NULL
-- );

-- CREATE TABLE IF NOT EXISTS todos (
--     id uuid PRIMARY KEY DEFAULT gen_random_uuid() NOT NULL,
--     userID uuid NOT NULL,
--     title TEXT NOT NULL,
--     created timestamptz NOT NULL,
--     due timestamptz NOT NULL,
--     completed BOOLEAN NOT NULL,
--     FOREIGN KEY (userID) references users
-- );