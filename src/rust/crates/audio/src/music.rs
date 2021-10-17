//! TODO:Describe what is different from audio

// TODO: Defines distance from A4. i8 looks like a good option so not sure if a
// custom struct is required.
// TODO: Technically this can be any type capable of adding +1/-1.
type Pitch = i8;

// TODO:
pub struct PitchSystem<T> {
    // TODO: concert pitch, pitch standard
    pub basic: T
}

// TODO: Standard concert pitch
pub const A440: PitchSystem::<f64> = PitchSystem::<f64> {
    basic: 440.0
};

// TODO:
pub trait PitchSynthesis<T> {
    fn synthesize(self, _: Pitch) -> T;
}

// TODO:
// impl<T: Div + From<U>, U> PitchSynthesis<T> for PitchSystem<T> {
//     fn synthesize(self, pitch: U) -> T {
//         // TODO: 2 ** (pitch / 12) * self.basic
//     }
// }

// TODO:Technically note can be considered as a basic pitch, accepting octave
// as an argument and modified by accidentals (sharp and flat), e.g. C(4).sharp.sharp.
// May be some unary operators can be used instead if sharp/flat.
// pub enum Note { C, D, E, F, G, A, H }
