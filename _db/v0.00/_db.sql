
--DROP TABLE IF EXISTS public."Transfer";
--DROP TABLE IF EXISTS public."Movements";
--DROP TABLE IF EXISTS public."Accounts";
--DROP TABLE IF EXISTS public."Tokens";
--DROP TABLE IF EXISTS public."Users";
--DROP TABLE IF EXISTS public."Documents";


-- public."Documents" definition

-- Drop table

-- DROP TABLE public."Documents";

CREATE TABLE public."Documents" (
	id serial4 NOT NULL,
	file_name varchar NOT NULL,
	file_type varchar NOT NULL,
	file bytea NOT NULL,
	account_id int4 NOT NULL,
	created_at timestamptz NOT NULL DEFAULT now(),
	CONSTRAINT documents_pkey PRIMARY KEY (id)
);


-- public."Transfer" definition

-- Drop table

-- DROP TABLE public."Transfer";

CREATE TABLE public."Transfer" (
	id serial4 NOT NULL,
	amount numeric NOT NULL,
	fromaccountid int4 NOT NULL,
	toaccountid int4 NOT NULL,
	created_at timestamptz NOT NULL DEFAULT now(),
	CONSTRAINT transfers_pkey PRIMARY KEY (id)
);


-- public."Users" definition

-- Drop table

-- DROP TABLE public."Users";

CREATE TABLE public."Users" (
	id serial4 NOT NULL,
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


-- public."Accounts" definition

-- Drop table

-- DROP TABLE public."Accounts";

CREATE TABLE public."Accounts" (
	id serial4 NOT NULL,
	user_id int4 NOT NULL,
	balance numeric NOT NULL,
	currency varchar NOT NULL,
	created_at timestamptz NOT NULL DEFAULT now(),
	CONSTRAINT accounts_pkey PRIMARY KEY (id),
	CONSTRAINT accounts_fkey FOREIGN KEY (user_id) REFERENCES public."Users"(id)
);


-- public."Movements" definition

-- Drop table

-- DROP TABLE public."Movements";

CREATE TABLE public."Movements" (
	id serial4 NOT NULL,
	accountid int4 NOT NULL,
	amount numeric NOT NULL,
	created_at timestamptz NOT NULL DEFAULT now(),
	CONSTRAINT movements_pkey PRIMARY KEY (id),
	CONSTRAINT movements_fkey FOREIGN KEY (accountid) REFERENCES public."Accounts"(id)
);


-- public."Tokens" definition

-- Drop table

-- DROP TABLE public."Tokens";

CREATE TABLE public."Tokens" (
	id serial4 NOT NULL,
	user_id int4 NOT NULL,
	created_at timestamptz NOT NULL DEFAULT now(),
	refresh_token varchar NULL,
	refresh_token_expire_at timestamptz NULL,
	CONSTRAINT tokens_pkey PRIMARY KEY (id),
	CONSTRAINT tokens_fkey FOREIGN KEY (user_id) REFERENCES public."Users"(id)
);