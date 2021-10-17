CREATE OR REPLACE VIEW mesh.mess_scheme_tags
AS SELECT
    tag.*,
    mess.url
FROM
    mess.mess,
    LATERAL XMLTABLE(
        ('//*'::text) PASSING (mess.content) COLUMNS
        name text PATH ('name(.)'::text),
        node xml PATH '.'
    ) tag
;
