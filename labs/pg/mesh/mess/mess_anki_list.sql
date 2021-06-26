CREATE VIEW mesh.mess_anki_list AS
SELECT
    mess.id,
    anki.question,
    anki.answer
FROM
    mess.mess,
     LATERAL XMLTABLE(
         ('//anki'::text) PASSING (mess.content) COLUMNS
         question text PATH ('@q'::text),
         answer text PATH ('text()'::text)
     ) anki
ORDER BY
    random()
;
