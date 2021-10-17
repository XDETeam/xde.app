CREATE OR REPLACE FUNCTION mesh.node_xslt_transform(node text, media text)
RETURNS text
AS $$
DECLARE
    _document xml;
    _xpath text;
    _links text[];
    _style xml;
    _result xml;
BEGIN
    _document = (SELECT content FROM mesh.node WHERE path = node_xslt_transform.node);

    _xpath = format(
        '//link[@rel="mesh:xslt" and @type="text/xsl" and @media="%s"]/@href',
        node_xslt_transform.media
    );
    _links = xpath(_xpath, _document);

    CASE cardinality(_links)
        WHEN 0 THEN RETURN NULL;
        WHEN 2 THEN RAISE EXCEPTION 'Multiple XSLT styles defined';
        ELSE
    END CASE;

    _style = (SELECT content FROM mesh.node WHERE path = _links[1]);
    IF _style IS NULL THEN
        RAISE EXCEPTION 'XSLT style "%" not found', _links[1];
    END IF;

    _result = xslt_process(_document::text, _style::text, '');

    RETURN _result;
END $$ LANGUAGE plpgsql;
