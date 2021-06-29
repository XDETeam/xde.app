CREATE TABLE mess.node (
    id bigserial NOT NULL
        constraint node_pk primary key ,
    url ltree NOT NULL,
    content xml NOT NULL,
    version integer NOT NULL default 1
);

CREATE UNIQUE INDEX node_url_uindex
    on mess.node (url)
;

CREATE INDEX
	node_content_ftsindex
ON
	mess.node
USING
	GIN ((content::text) gin_trgm_ops)
;
