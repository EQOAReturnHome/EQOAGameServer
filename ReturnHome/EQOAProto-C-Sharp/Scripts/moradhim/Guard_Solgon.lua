function  event_say(choice)
diagOptions = {}
    npcDialogue = "Mind yer step here in Sorenson's Keep."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end