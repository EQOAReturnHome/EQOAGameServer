function event_say()
diagOptions = {}
    npcDialogue = "I recently discovered a tomb in the south while hunting orcs. An old ranger hero known as Vitsh'Sah once protected the elves. After he died in battle, his remains were moved to the tomb to be safe when the elves moved north. The tomb was once sealed, but I believe that the orcs have somehow disturbed it. I confess, I have such an eerie feeling being near it... Is that what will become of me?"
SendDialogue(mySession, npcDialogue, diagOptions)
end