function  event_say(choice)
diagOptions = {}
    npcDialogue = "We Defenders of Erollisi Marr shall fight back against the Plague Bringer of Bertoxxulous. We shall purify this land, and make it a new world with Errollisi's blessing."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end