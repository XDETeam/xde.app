--TODO:Probably won't be part of the deployment script
INSERT INTO mesh.node (
    path,
	content
)
VALUES 
(
'//xde/default.xsl',
'<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:template match="/">
		TODO:
	</xsl:template>
</xsl:stylesheet>'
),

(
'//xde/labs/db-version',
'<root></root>'
)
;

select mesh.node_xslt_transform('//xde/labs/db-version', '//xde/default.xsl');