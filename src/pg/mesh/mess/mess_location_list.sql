CREATE OR REPLACE VIEW mesh.mess_location_list
AS
SELECT
	location.at AS location,
    location.title,
    mess.id,
    mess.content
FROM
	mess.mess,
	LATERAL XMLTABLE(('//location'::text)
		PASSING (mess.content)
		COLUMNS at text PATH ('@at'::text), title text PATH ('../@title'::text)) location
ORDER BY
	location.at
;
