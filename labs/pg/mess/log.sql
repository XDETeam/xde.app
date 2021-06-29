CREATE TABLE mess.log (
    id bigint not null,
    version integer NOT NULL,
    author text NOT NULL,
    url ltree NOT NULL,
    content xml NULL
);

ALTER TABLE
    mess.log
ADD CONSTRAINT
    log_pk
PRIMARY KEY (
    id,
    version
);
