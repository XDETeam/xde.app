CREATE OR REPLACE FUNCTION mesh.node_xslt_transform(node text, style text)
RETURNS text
AS $$
DECLARE
    _document xml;
    _style xml;
    _result xml;
BEGIN
    _document = (SELECT content FROM mesh.node WHERE path = node_xslt_transform.node);
    _style = (SELECT content FROM mesh.node WHERE path = node_xslt_transform.style);
    _result = xslt_process(_document::text, _style::text, '');

    RETURN _result;
END $$ LANGUAGE plpgsql;
