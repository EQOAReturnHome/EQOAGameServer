function  event_say(choice)
diagOptions = {}
    npcDialogue = "My father is too trusting for times like these. With all the bandits about who knows what kind of folk he might invite inside."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end