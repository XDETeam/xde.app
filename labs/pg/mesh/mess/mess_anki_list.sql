create view mess_anki_list(id, question, answer) as
SELECT mess.id,
       anki.question,
       anki.answer
FROM mess.mess,
     LATERAL XMLTABLE(('//anki'::text) PASSING (mess.content)
                      COLUMNS question text PATH ('@q'::text), answer text PATH ('text()'::text)) anki
ORDER BY (random());

alter table mess_anki_list
    owner to postgres;

grant delete, insert, references, select, trigger, truncate, update on mess_anki_list to yavulan;

