CREATE OR REPLACE VIEW mesh.mess_scheme_tags
AS SELECT
    tag.*,
    mess.id
FROM
    mess.mess,
    LATERAL XMLTABLE(
        ('//*'::text) PASSING (mess.content) COLUMNS
        name text PATH ('name(.)'::text),
        node xml PATH '.'
    ) tag
;
