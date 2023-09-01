function  event_say(choice)
diagOptions = {}
    npcDialogue = "The sheer number of wolves and bears in this forest is quite unusual this time of year. The great bears have been passing through the village at night."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end