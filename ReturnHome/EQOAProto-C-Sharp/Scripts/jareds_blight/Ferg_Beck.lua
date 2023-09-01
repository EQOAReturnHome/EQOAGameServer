function  event_say(choice)
diagOptions = {}
    npcDialogue = "Bandits, sorcerers, undead and wildlife all passing through here. Mister Dosier pays me well enough to keep the Blackswan Inn secure. Now excuse me, playerName, I've work to do."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end