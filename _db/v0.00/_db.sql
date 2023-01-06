DROP TABLE IF EXISTS public."Transfer";
DROP TABLE IF EXISTS public."Accounts";
DROP TABLE IF EXISTS public."Tokens";
DROP TABLE IF EXISTS public."Users";
DROP TABLE IF EXISTS public."Documents";



CREATE TABLE "Users" (
	id serial NOT NULL,
	username varchar NOT NULL,
	"password" varchar NOT NULL,
	full_name varchar NOT NULL,
	email varchar NOT NULL,
	password_changed_at timestamptz NOT NULL DEFAULT now(),
	created_at timestamptz NOT NULL DEFAULT now(),
	CONSTRAINT users_email_key UNIQUE (email),
		CONSTRAINT users_pkey PRIMARY KEY (id),
	CONSTRAINT users_username UNIQUE (username)
);

CREATE TABLE "Accounts" (
	id serial NOT NULL,
	user_id int NOT NULL,
	balance decimal NOT NULL,
	currency varchar NOT NULL,
	created_at timestamptz NOT NULL DEFAULT now(),
	CONSTRAINT accounts_pkey PRIMARY KEY (id),
	CONSTRAINT accounts_fkey FOREIGN KEY(user_id) REFERENCES "Users"(id)
);


create table "Transfer" (
	id serial NOT NULL,
	amount decimal NOT NULL,
	fromAccountId int NOT NULL,
	toAccountId int NOT NULL,
	created_at timestamptz NOT NULL DEFAULT now(),
	CONSTRAINT transfers_pkey PRIMARY KEY (id)
);

CREATE TABLE "Tokens" (
   	id serial NOT NULL,
    user_id int NOT NULL,
    created_at timestamptz NOT NULL DEFAULT now(),
    refresh_token varchar,
    refresh_token_expire_at timestamptz,
    CONSTRAINT tokens_pkey PRIMARY KEY (id),
    CONSTRAINT tokens_fkey FOREIGN KEY(user_id) REFERENCES "Users"(id) 
);


CREATE TABLE "Documents" (
   	id serial NOT NULL,
	file_name varchar NOT NULL,
	file_type varchar NOT NULL,
	file bytea NOT NULL,
	account_id int NOT NULL,
	created_at timestamptz NOT NULL DEFAULT now(),
	CONSTRAINT documents_pkey PRIMARY KEY (id)
);
