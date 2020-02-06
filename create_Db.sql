-- Database: "PumTestProject"

-- DROP DATABASE "PumTestProject";

 DROP ROLE IF EXISTS pumtest;
  CREATE ROLE pumtest LOGIN
  UNENCRYPTED PASSWORD 'test'
  SUPERUSER INHERIT CREATEDB CREATEROLE REPLICATION;


  ALTER DEFAULT PRIVILEGES
    GRANT INSERT, SELECT, UPDATE, DELETE, TRUNCATE, REFERENCES, TRIGGER ON TABLES
    TO pumtest;
  


CREATE DATABASE "PumTestProject"
  WITH OWNER = postgres
       ENCODING = 'UTF8'
       TABLESPACE = pg_default
       CONNECTION LIMIT = -1;
	   
	   ALTER DEFAULT PRIVILEGES
    GRANT INSERT, SELECT, UPDATE, DELETE, TRUNCATE, REFERENCES, TRIGGER ON TABLES
    TO pumtest;

\connect PumTestProject
	   
	   -- Table: public.companies

-- DROP TABLE public.companies;

CREATE SEQUENCE companies_id_seq;
  GRANT USAGE ON SEQUENCE companies_id_seq TO pumtest;
  
  CREATE SEQUENCE employees_id_seq;
  GRANT USAGE ON SEQUENCE employees_id_seq TO pumtest;

  

CREATE TABLE public.companies
(
  id bigint NOT NULL DEFAULT nextval('companies_id_seq'::regclass),
  name text NOT NULL,
  establishmentyear integer NOT NULL,
  CONSTRAINT "PK_public.companies" PRIMARY KEY (id)
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.companies
  OWNER TO postgres;
  
  -- Table: public.employees

-- DROP TABLE public.employees;

CREATE TABLE public.employees
(
  id bigint NOT NULL DEFAULT nextval('employees_id_seq'::regclass),
  name text NOT NULL,
  surname text NOT NULL,
  birthdate timestamp without time zone NOT NULL,
  jobtitle integer NOT NULL,
  "Company_Id" bigint NOT NULL,
  CONSTRAINT "PK_public.employees" PRIMARY KEY (id),
  CONSTRAINT "FK_public.employees_public.companies_Company_Id" FOREIGN KEY ("Company_Id")
      REFERENCES public.companies (id) MATCH SIMPLE
      ON UPDATE NO ACTION ON DELETE CASCADE
)
WITH (
  OIDS=FALSE
);
ALTER TABLE public.employees
  OWNER TO postgres;

-- Index: public."employees_IX_Company_Id"

-- DROP INDEX public."employees_IX_Company_Id";

CREATE INDEX "employees_IX_Company_Id"
  ON public.employees
  USING btree
  ("Company_Id");