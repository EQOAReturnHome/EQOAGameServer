function  event_say(choice)
diagOptions = {}
    npcDialogue = "The Frogloks make friends with the fungus creatures in Guk. This many more enemies for trolls to smush."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end