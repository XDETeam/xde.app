CREATE TABLE mess.log (
    id bigint not null,
    created timestamp NOT NULL
		DEFAULT(now() at time zone 'utc'),
    version integer NOT NULL,
    author text NOT NULL,
    url ltree NOT NULL,
    content xml NULL,
    notes xml NULL
);

ALTER TABLE
    mess.log
ADD CONSTRAINT
    log_pk
PRIMARY KEY (
    id,
    version
);
