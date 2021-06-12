CREATE VIEW mess.q_view
AS SELECT
       url,
       xt.content
FROM
        mess.mesh,
        XMLTABLE('//q[not(ancestor::story)]' PASSING content COLUMNS "content" XML PATH 'node()') xt
;