function  event_say(choice)
diagOptions = {}
    npcDialogue = "Crethley Manor to the southwest is overflowing with undead. I won't be going anywhere near there, no matter how much they pay me."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end