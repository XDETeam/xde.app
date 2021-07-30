CREATE OR REPLACE VIEW mesh.mess_tags_list
AS
SELECT
	DISTINCT unnest(string_to_array(tags.tags, ','::text)) AS tag,
	mess.url
FROM
	mess.mess,
    LATERAL XMLTABLE(('//*[@tags]'::text) PASSING (mess.content) COLUMNS tags text PATH ('@tags'::text)) tags
ORDER BY
	unnest(string_to_array(tags.tags, ','::text))
;
