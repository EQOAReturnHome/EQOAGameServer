function  event_say(choice)
diagOptions = {}
    npcDialogue = "Keep yer business short. I've got reports to review, \"construction\" product to move, and pockets to lighten."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end