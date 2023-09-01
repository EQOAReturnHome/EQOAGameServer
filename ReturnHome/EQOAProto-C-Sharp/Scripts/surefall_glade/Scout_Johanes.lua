function  event_say(choice)
diagOptions = {}
    npcDialogue = "Some of the gnolls have migrated to a mountain just beyond Jethro's Lake to the west. They are just as vicious, but possibly even more organized than the Sleshers."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end