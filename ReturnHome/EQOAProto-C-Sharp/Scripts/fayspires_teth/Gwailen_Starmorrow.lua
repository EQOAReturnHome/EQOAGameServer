function event_say()
diagOptions = {}
    npcDialogue = "As one of the most powerful enchanters of Fayspires, it is my duty to protect the city by ensuring the mana that flows through it remains steady. As part of the construction of the city, Winter's Deep Lake contains a great deal of self generating mana. We enchanters channel the power through the spires of the city, which helps to protect all elves that reside here."
SendDialogue(mySession, npcDialogue, diagOptions)
end