CREATE TABLE env.version (
	id int NOT NULL,
		CONSTRAINT pk_env_version PRIMARY KEY(id),
	
	installed timestamp NOT NULL
		DEFAULT(now() at time zone 'utc')
);
