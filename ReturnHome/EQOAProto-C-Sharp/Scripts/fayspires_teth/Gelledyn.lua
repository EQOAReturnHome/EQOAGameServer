function event_say()
diagOptions = {}
    npcDialogue = "Left unchecked, the vampires of this world will wait in hiding, strike out in the dark, and slowly suck the blood of all living beings. We must do all that we can to hunt them down, and end their rein of terror."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end