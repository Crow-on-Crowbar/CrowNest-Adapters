#pragma once

#include <iostream>
#include "../CrowSDK/IAdapter.h"
#include "../CrowCommon/Logger.h"

class TemplateAdapter : public IAdapter
{
private:
	Logger _logger;

public:
	void Attach() override;
	void Detach() override;
	std::string GetGameName() override;
};