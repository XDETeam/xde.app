CREATE OR REPLACE VIEW mesh.mess_q_view
AS SELECT
	mess.id,
	xt.content
FROM
    mess.mess,
    XMLTABLE('//q[not(ancestor::story)]' PASSING content COLUMNS "content" XML PATH 'node()') xt
;
