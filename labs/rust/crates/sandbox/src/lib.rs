use syn::{
    Type,
    parse::{ Result, Parse, ParseStream }
};

// This is a sample of a custom structure parsing
mod keywords {
    syn::custom_keyword!(Given);
}

pub struct GivenStatement {
    pub object: Type,
}

impl Parse for GivenStatement {
    fn parse(input: ParseStream) -> Result<Self> {
        //input.parse::<Token![Given]>()?;
        input.parse::<keywords::Given>()?;

        let object: Type = input.parse()?;

        Ok(GivenStatement {
            object,
        })
    }
}
