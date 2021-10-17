CREATE OR REPLACE VIEW mesh.mess_tags
AS
SELECT
	mess.id,
	unnest.unnest AS tag
FROM
	mess.mess,
    LATERAL unnest(string_to_array((xpath('//@tags'::text, mess.content))[1]::text, ','::text)) unnest(unnest)
;
