function event_say()
diagOptions = {}
    npcDialogue = "Generations ago, our family made the finest royal robes for the king and queen of the elves, and many of the other royal families. We have learned to adapt and survive, but we proudly offer our services here in Fayspires."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end