INCLUDE Globals.ink

{General1Talked:-> AfterTalk|-> Main}
=== Main ===
~General1Talked=true

Ehi, ehi, ehi. Pivello, che ci fai qui?

* Non lo so, mi ci sono ritrovato per caso.
    Pivello, come fai a non sapere il tuo compito?!
    -> GiveLetter

* Mi facevo un giro.
    Un giro? Un GIRO?! Qui siamo in guerra, non al mercato.
    -> GiveLetter

* Fatti gli affari tuoi, vecchio.
    Pivello, sei carne morta, ma ringrazia che hai un compito da svolgere.
    -> GiveLetter

=== GiveLetter ===
Prendi questa lettera e portala al generale Pdor. Subito.
-> END

=== AfterTalk ===
Muoviti. Porta quella lettera al generale Pdor.
-> END